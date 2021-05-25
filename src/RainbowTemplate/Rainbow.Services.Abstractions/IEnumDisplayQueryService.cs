using Rainbow.ViewModels;
using System;
using System.Collections.Generic;
using Yunyong.Core;

namespace Rainbow.Services
{
    public interface IEnumDisplayQueryService
    {
        AsyncTaskTResult<List<EnumDisplayVM>> GetEnumDisplayList();
        AsyncTaskTResult<EnumDisplayVM> GetEnumDisplay(string name);
        void Register<EType>(EnumDisplayVM vm) where EType : Enum;
        void Register<EType>(Dictionary<EType, string> dataDic, string display = default) where EType : Enum;
        void Register<EType>() where EType : Enum;
    }
}