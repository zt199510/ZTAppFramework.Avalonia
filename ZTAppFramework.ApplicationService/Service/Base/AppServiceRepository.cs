using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTAppFramework.ApplicationService.Stared;
using ZTAppFramework.ApplicationService.Stared.Attributes;
using ZTAppFramework.ApplicationService.Stared.HttpManager;

namespace ZTAppFramework.ApplicationService
{
    public class AppServiceRepository<T> : AppServiceBase
    {
        public AppServiceRepository(ApiClinetRepository apiClinet) : base(apiClinet)
        {

        }

        [ApiUrl("List")]
        public async Task<AppliResult<List<T>>> GetList(string Key = "")
        {
            AppliResult<List<T>> result = new AppliResult<List<T>>();

            var api = await _apiClinet.GetAsync<List<T>>(GetEndpoint(), new { Key = Key });
            if (api.success)
            {
                if (api.Code == 200)
                {
                    result.Success = true;
                    result.data = api.data;
                    result.Message = api.message;
                }
                else
                {
                    result.Success = false;
                    result.Message = api.message;
                }
            }
            else
            {
                result.Success = false;
                result.Message = api.message;
            }
            return result;
        }
        [ApiUrl("")]
        public async Task<AppliResult<bool>> Delete(string Id)
        {
            AppliResult<bool> result = new AppliResult<bool>();
            var api = await _apiClinet.DeleteAsync<bool>(GetEndpoint(), new { Ids = Id });
            if (api.success && api.Code == 200)
            {
                result.Success = true;
                result.data = api.data;
                result.Message = api.message;
            }
            else
            {
                result.Success = false;
                result.Message = api.message;
            }
            return result;
        }


        [ApiUrl("")]
        public async Task<AppliResult<bool>> Add(T Param)
        {
            AppliResult<bool> result = new AppliResult<bool>();

            var api = await _apiClinet.PostAsync<bool>(GetEndpoint(), Param);
            if (api.success)
            {
                if (api.Code == 200)
                {
                    result.Success = true;
                    result.Message = "添加成功";
                }
                else
                {
                    result.Success = false;
                    result.Message = api.message;
                }
            }
            else
            {
                result.Success = false;
                result.Message = api.message;
            }
            return result;
        }

        [ApiUrl("")]
        public virtual async Task<AppliResult<bool>> Modif<TModel>(TModel Param)
        {
            AppliResult<bool> result = new AppliResult<bool>();

            var api = await _apiClinet.PutAsync<bool>(GetEndpoint(), Param);
            if (api.success)
            {
                if (api.Code == 200)
                {
                    result.Success = true;
                    result.Message = "修改成功";
                }
                else
                {
                    result.Success = false;
                    result.Message = api.message;
                }
            }
            else
            {
                result.Success = false;
                result.Message = api.message;
            }
            return result;
        }

        [ApiUrl("")]
        public virtual async Task<AppliResult<bool>> Add<TModel>(TModel Param)
        {
            AppliResult<bool> result = new AppliResult<bool>();

            var api = await _apiClinet.PostAsync<bool>(GetEndpoint(), Param);
            if (api.success)
            {
                if (api.Code == 200)
                {
                    result.Success = true;
                    result.Message = "添加成功";
                }
                else
                {
                    result.Success = false;
                    result.Message = api.message;
                }
            }
            else
            {
                result.Success = false;
                result.Message = api.message;
            }
            return result;
        }
    }
}
