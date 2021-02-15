import Vue from 'vue';
import User from '../store/user';
import Router from 'vue-router';
import Main from './../pages/Main';
import Login from './../pages/Login';
import Register from './../pages/Register';
import Profile from './../pages/Profile';

import Blank from './../pages/Blank';
import Room from './../pages/Room/Room.vue';
import Movies from './../pages/Movies/Movies.vue';
import MoviesCreate from './../pages/Movies/Create.vue';
import Sessions from './../pages/Sessions/Sessions.vue';
import SessionCreate from './../pages/Sessions/Create.vue';

Vue.use(Router);

const router = new Router({
  mode: 'history',
  routes: [
    {
      path: '/',
      component: Main,
      children: [
        { path: '/', component: Blank },
        { path: 'profile', component: Profile },
        { path: '/room', component: Room },
        { path: '/movies', component: Movies },
        { path: '/movies/:id', component: MoviesCreate },
        { path: '/sessions', component: Sessions },
        { path: '/sessions/:id', component: SessionCreate },
      ],
    },
    {
      path: '/login',
      component: Login,
    },
    {
      path: '/register',
      component: Register,
    },
  ],
});
var freeRoutes = ['/login', '/register'];

router.beforeEach((to, from, next) => {
  if (!freeRoutes.includes(to.path) && !User.hasToken()) {
    next('/login');
  } else {
    next();
  }
});

export default router;
