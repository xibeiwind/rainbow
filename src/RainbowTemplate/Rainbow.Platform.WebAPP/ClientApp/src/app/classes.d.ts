
declare namespace Rainbow.Common.Enums {
}
declare namespace Rainbow.ViewModels.Messages {
	interface MessageQueryOption extends Yunyong.Core.PagingQueryOption {
	}
	interface MessageVM extends Yunyong.Core.ViewModels.VMBase {
		SendFrom?: string;
		SendFromId: string;
		SendOn: Date;
		SendTo?: string;
		SendToId: string;
		Summary?: string;
		Title?: string;
	}
}
declare namespace Rainbow.ViewModels.Tasks {
	interface TaskVM {
		Progress: number;
		State: Rainbow.Common.Enums.TaskState;
		Title?: string;
	}
}
declare namespace Rainbow.ViewModels.Users {
	interface ForgetPasswordVM {
	}
	interface LoginResultVM {
		IsSuccess: boolean;
		Message?: string;
		UserId?: any;
	}
	interface LoginVM {
	}
	interface RegisterUserVM extends Yunyong.Core.ViewModels.CreateVM {
	}
	interface UserVM {
	}
}
declare namespace Yunyong.Core {
	interface AsyncTaskResult {
		ErrorMessage?: string;
		Status: Yunyong.Core.AsyncTaskStatus;
	}
	interface AsyncTaskTResult<T> extends Yunyong.Core.AsyncTaskResult {
		Data?: T;
	}
	interface OrderBy {
		Desc: boolean;
		Field?: string;
	}
	interface PagingList<TEntity> {
		Data?: TEntity[];
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
		OrderBys?: Yunyong.Core.OrderBy[];
	}
}
declare namespace Yunyong.Core.ViewModels {
	interface CreateVM {
	}
	interface VMBase {
		Id: string;
	}
}
