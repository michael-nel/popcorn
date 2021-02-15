import Vue from 'vue';

export function showSuccess(message) {
  Vue.swal({
    toast: true,
    position: 'top-end',
    title: message,
    icon: 'success', //built in icons: success, warning, error, info
    timer: 3000, //timeOut for auto-close
    timerProgressBar: true,
    showConfirmButton: false,
  });
}

export function showWarning(message) {
  Vue.swal({
    toast: true,
    position: 'top-end',
    title: message,
    icon: 'warning', //built in icons: success, warning, error, info
    timer: 3000, //timeOut for auto-close
    timerProgressBar: true,
    showConfirmButton: false,
  });
}
export function showError(message) {
  Vue.swal({
    toast: true,
    position: 'top-end',
    title: message,
    icon: 'error', //built in icons: success, warning, error, info
    timer: 3000, //timeOut for auto-close
    timerProgressBar: true,
    showConfirmButton: false,
  });
}

export default { showError, showSuccess };
