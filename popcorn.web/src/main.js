import Vue from 'vue';
import Vuex from 'vuex';
import App from './App.vue';

import router from './router';
import store from './store';
import User from './store/user';
import { VuejsDatatableFactory } from 'vuejs-datatable';
import VueSweetalert2 from 'vue-sweetalert2';
import 'sweetalert2/dist/sweetalert2.min.css';

Vue.config.productionTip = false;
Vue.use(Vuex);
Vue.use(VuejsDatatableFactory);
Vue.use(VueSweetalert2);

window.User = User;
Vue.prototype.$userDevelop = 'teste@teste.com.br';
Vue.prototype.$passwordDevelop = '12345';
Vue.prototype.$develop = false;

new Vue({
  store: new Vuex.Store(store),
  router: router,
  render: h => h(App),
}).$mount('#app');
