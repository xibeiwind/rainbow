import { GlobalConfig, Toast } from 'ngx-toastr';

export const toastrConfig: GlobalConfig = {
  maxOpened: 0,
  autoDismiss: false,
  iconClasses: {
    success: 'toast-success',
    info: 'toast-info',
    error: 'toast-error',
    warning: 'toast-warning'
  },
  countDuplicates: false,
  newestOnTop: true,
  preventDuplicates: false,
  resetTimeoutOnDuplicate: false,
  disableTimeOut: false,
  timeOut: 1000,
  closeButton: false,
  extendedTimeOut: 1000,
  includeTitleDuplicates: false,
  progressBar: false,
  progressAnimation: 'decreasing',
  enableHtml: false,
  toastClass: 'ngx-toastr',
  positionClass: 'toast-bottom-right',
  titleClass: 'toast-title',
  messageClass: 'toast-message',
  easing: 'ease-in',
  easeTime: 300,
  tapToDismiss: true,
  toastComponent: Toast,
  onActivateTick: false
};
