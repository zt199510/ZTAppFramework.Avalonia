using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.ApplicationService.Stared.HttpManager;
using ZTAppFramework.Avalonia.Sqlite.Utils;
using ZTAppFramework.Sqlite.Implements;
using ZTAppFramework.Sqlite.Models;
using ZTAppFramewrok.Application.Stared;

namespace ZTAppFramework.ApplicationService.Service
{
    public class SysAdminService : AppServiceRepository<SysAdminDto>
    {
        /// <summary>
        /// Url
        /// </summary>
        public override string ApiServiceUrl => "/api/SysAdmin";

        #region 服务
        private readonly UserLocalSerivce _userLocalSerivce;
        private readonly KeyConfigLocalService _keyConfigLocalService;
        #endregion

        public SysAdminService(ApiClinetRepository apiClinet, UserLocalSerivce userLocalSerivce,KeyConfigLocalService keyConfigLocalService) : base(apiClinet)
        {
            _userLocalSerivce=userLocalSerivce;
            _keyConfigLocalService = keyConfigLocalService;
        }

        /// <summary>
        /// 获取账号存储信息
        /// </summary>
        /// <returns></returns>
        public async Task<AppliResult<KeyConfig>> GetLocalAccountInfo()
        {
            var r = await _keyConfigLocalService.GetUserSaveInfo();
            return new AppliResult<KeyConfig>() { data = r.data };
        }


        /// <summary>
        /// 获取账号记录
        /// </summary>
        /// <returns></returns>
        public async Task<AppliResult<List<LoginParam>>> GetLocalAccountList()
        {
            AppliResult<List<LoginParam>> result = new AppliResult<List<LoginParam>>();
            var Csql = await _userLocalSerivce.GetListAsync();
            if (Csql.success)
            {
                result.data = new List<LoginParam>();
                Csql.data.ForEach(x => result.data.Add(new LoginParam() { Account = x.Name, Password = x.Password }));
            }
            else
            {
                result.Success = false;
            }

            return result;
        }
        public async Task<AppliResult<string>> SaveLocalAccountInfo(bool Save, LoginParam user)
        {
            AppliResult<string> result = new AppliResult<string>();

            var d = await _keyConfigLocalService.GetModelAsync(x => x.Key == AppKeys.SaveUserInfoKey);
            if (d.data != null)
            {
                d.data.Values = user.Account;
                d.data.Check = Save;
                var r = await _keyConfigLocalService.UpdateAsync(d.data);
                if (r.success)
                    result.Message = "保存成功";
            }
            return result;
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<AppliResult<string>> LoginServer(LoginParam user)
        {
            AppliResult<string> res = new AppliResult<string>() { Success = false, Message = "未知异常" };
            ApiResult<LoginTokenDto> api = await _apiClinet.PostAnonymousAsync<LoginTokenDto>("/api/Operator/Login", user);
            if (api.success)
            {
                if (api.Code == 200)
                {
                    res.Success = true;
                    res.Message = "登入成功";
                    var info = await _userLocalSerivce.GetModelAsync(x => x.Name == user.Account);
                    if (info.data == null)
                    {
                        var CSql = await _userLocalSerivce.AddAsync(new Sqlite.Models.Account() { Name = user.Account, Password = user.Password });
                    }
                    else
                    {
                        info.data.Password = user.Password;
                        var CSql = await _userLocalSerivce.UpdateAsync(info.data);
                    }
                    _apiClinet.SetToken(api.data);
                }
                else
                {
                    res.Message = api.message;
                }
            }
            else
            {
                res.Message = api.message;
            }
            return res;
        }


    }
}
