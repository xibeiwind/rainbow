
declare namespace Rainbow.Common.Enums {
}
declare namespace Rainbow.ViewModels {
	interface DisplayQueryVM {
		Name: string;
	}
	interface FieldDisplayVM {
		DisplayName: string;
		Name: string;
	}
	interface ModelDisplaySuitVM {
		DisplayName: string;
		ModelName: string;
		ViewModels: Rainbow.ViewModels.ViewModelDisplayVM[];
	}
	interface ViewModelDisplayVM {
		DisplayName: string;
		Fields: Rainbow.ViewModels.FieldDisplayVM[];
		ModelName: string;
		Name: string;
		Type: Rainbow.Common.Enums.VMType;
	}
}
declare namespace Rainbow.ViewModels.Messages {
	interface MessageQueryOption extends Yunyong.Core.PagingQueryOption {
	}
	interface MessageVM extends Yunyong.Core.ViewModels.VMBase {
		SendFrom: string;
		SendFromId: string;
		SendOn: Date;
		SendTo: string;
		SendToId: string;
		Summary: string;
		Title: string;
	}
}
declare namespace Rainbow.ViewModels.Models {
	interface CreateModelSuitApplyVM {
		EnableDelete: boolean;
		FolderName: string;
		GenerateController: boolean;
		GenerateNgModuleComponent: boolean;
		GenerateService: boolean;
		Items: Rainbow.ViewModels.Models.CreateViewModelApplyVM[];
		ModelFullName: string;
		ModelName: string;
		UpdateTsServices: boolean;
	}
	interface CreateViewModelApplyVM {
		ActionName: string;
		DisplayName: string;
		Fields: string[];
		Name: string;
		Type: Rainbow.Common.Enums.VMType;
	}
	interface FieldVM {
		DisplayName: string;
		Name: string;
		Type: string;
	}
	interface ModelTypeVM {
		Asssembly: string;
		DisplayName: string;
		Fields: Rainbow.ViewModels.Models.FieldVM[];
		FullName: string;
		Name: string;
	}
}
declare namespace Rainbow.ViewModels.Tasks {
	interface NotifyQueryOption extends Yunyong.Core.PagingQueryOption {
		Type: Rainbow.Common.Enums.NotifyType;
	}
	interface NotifyVM extends Yunyong.Core.ViewModels.VMBase {
		Title: string;
		Type: Rainbow.Common.Enums.NotifyType;
	}
	interface TaskVM extends Yunyong.Core.ViewModels.VMBase {
		Progress: number;
		State: Rainbow.Common.Enums.TaskState;
		Title: string;
	}
}
declare namespace Rainbow.ViewModels.Users {
	interface LoginResultVM {
		IsSuccess: boolean;
		Message: string;
		UserId: string;
	}
	interface LoginVM {
		Password: string;
		Phone: string;
	}
	interface RegisterUserVM extends Yunyong.Core.ViewModels.CreateVM {
		Name: string;
		Phone: string;
	}
	interface SendLoginSmsVM {
		Phone: string;
	}
	interface SmsLoginVM {
		Phone: string;
		SmsCode: string;
	}
	interface UserVM extends Yunyong.Core.ViewModels.VMBase {
		Name: string;
	}
}
declare namespace Rainbow.ViewModels.Utils {
	interface PhoneSmsVM {
		Code: string;
		CodeType: Rainbow.Common.Enums.TplType;
		CreateOn: Date;
		Phone: string;
	}
	interface SendSmsRequestVM {
		CodeType: Rainbow.Common.Enums.TplType;
		Phone: string;
	}
	interface SendSmsResultVM {
		Message: string;
		SmsCode: string;
		State: boolean;
	}
	interface VerfyCodeNumLimitVM {
		ErrorNum: number;
		IsLocked: boolean;
		Phone: string;
	}
	interface VerifyingSmsCodeRequestVM {
		Code: string;
		CodeType: Rainbow.Common.Enums.TplType;
		Phone: string;
	}
	interface VerifyingSmsCodeResultVM {
		Message: string;
		State: boolean;
		Token: string;
	}
	interface VerifySmsSuccessVM {
		CodeType: Rainbow.Common.Enums.TplType;
		CreateOn: Date;
		Id: string;
		IsEnable: boolean;
		Phone: string;
		Token: string;
	}
}
declare namespace Yunyong.Core {
	interface AsyncTaskResult {
		ErrorMessage: string;
		Status: Yunyong.Core.AsyncTaskStatus;
	}
	interface AsyncTaskTResult<T> extends Yunyong.Core.AsyncTaskResult {
		Data: T;
	}
	interface OrderBy {
		Desc: boolean;
		Field: string;
	}
	interface PagingList<TEntity> {
		Data: TEntity[];
		PageIndex: number;
		PageSize: number;
		TotalCount: number;
		TotalPage: number;
	}
	interface PagingQueryOption extends Yunyong.Core.QueryOption {
		PageIndex: number;
		PageSize: number;
	}
	interface QueryOption {
		OrderBys: Yunyong.Core.OrderBy[];
	}
}
declare namespace Yunyong.Core.ViewModels {
	interface CreateVM {
	}
	interface VMBase {
		Id: string;
	}
}
