using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZTAppFramewrok.Application.Stared;


namespace ZTAppFramework.ApplicationService.Service
{
    /// <summary>
    ///********************************************
    /// 创建人        ：  WeiXiaolei
    /// 创建时间    ：  2022/9/28 14:04:19 
    /// Description   ：  
    ///********************************************/
    /// </summary>
    //public class SysMenuService : AppServiceBase
    //{
    //    public override string ApiServiceUrl => "/api/SysMenu";
    //    public SysMenuService(ApiClinetRepository apiClinet) : base(apiClinet)
    //    {

    //    }
     
    //    [ApiUrl("List")]
    //    public async Task<AppliResult<List<SysAdminDto>>> GetList()
    //    {
    //        AppliResult<List<SysAdminDto>> result = new AppliResult<List<SysAdminDto>>();
    //        var api = await _apiClinet.GetAsync<List<SysAdminDto>>(GetEndpoint());
    //        if (api.success && api.Code == 200)
    //        {
    //            result.Success = true;
    //            result.Message = api.message;
    //            result.data = api.data;
    //        }
    //        else
    //        {
    //            result.Success = false;
    //            result.Message = api.message;
    //        }
    //        return result;
    //    }

    //    [ApiUrl("Pages")]
    //    public async Task<AppliResult<PageResult<SysPostDto>>> GetPostList(PageParam pageParam)
    //    {
    //        AppliResult<PageResult<SysPostDto>> result = new AppliResult<PageResult<SysPostDto>>();
    //        var api = await _apiClinet.GetAsync<PageResult<SysPostDto>>(GetEndpoint(), pageParam);
    //        if (api.success && api.Code == 200)
    //        {
    //            result.Success = true;
    //            result.Message = api.message;
    //            result.data = api.data;
    //        }
    //        else
    //        {
    //            result.Success = false;
    //            result.Message = api.message;
    //        }
    //        return result;
    //    }

    //    [ApiUrl("")]
    //    public async Task<AppliResult<bool>> Add(SysPostParm Param)
    //    {
    //        AppliResult<bool> result = new AppliResult<bool>();

    //        var api = await _apiClinet.PostAsync<bool>(GetEndpoint(), Param);
    //        if (api.success && api.Code == 200)
    //        {
    //            result.Success = true;
    //            result.Message = "添加成功";
    //        }
    //        else
    //        {
    //            result.Success = false;
    //            result.Message = api.message;
    //        }
    //        return result;
    //    }

    //    public async Task<AppliResult<bool>> Modif(SysPostParm Param)
    //    {
    //        AppliResult<bool> result = new AppliResult<bool>();
    //        var api = await _apiClinet.PutAsync<bool>(GetEndpoint(), Param);
    //        if (api.success && api.Code == 200)
    //        {
    //            result.Success = true;
    //            result.Message = "添加成功";
    //        }
    //        else
    //        {
    //            result.Success = false;
    //            result.Message = api.message;
    //        }
    //        return result;
    //    }

    //    [ApiUrl("")]
    //    public async Task<AppliResult<bool>> Delete(string Id)
    //    {
    //        AppliResult<bool> result = new AppliResult<bool>();
    //        var api = await _apiClinet.DeleteAsync<bool>(GetEndpoint(), new { Ids = Id });
    //        if (api.success && api.Code == 200)
    //        {
    //            result.Success = true;
    //            result.data = api.data;
    //            result.Message = api.message;
    //        }
    //        else
    //        {
    //            result.Success = false;
    //            result.Message = api.message;
    //        }

    //        return result;
    //    }

    //    [ApiUrl("")]
    //    public async Task<AppliResult<SysMenuDto>> Query(string id)
    //    {
    //        AppliResult<SysMenuDto> result = new AppliResult<SysMenuDto>();
    //        var api = await _apiClinet.GetAsync<SysMenuDto>(GetEndpoint()+ id);
    //        if (api.success && api.Code == 200)
    //        {
    //            result.Success = true;
    //            result.data = api.data;
    //        }
    //        else
    //        {
    //            result.Message = api.message;
    //            result.Success = false;
    //        }
    //        return result;
    //    }
    //}
}
