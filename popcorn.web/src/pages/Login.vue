<template>
  <div class="login-box">
    <div class="login-logo" style="margin-bottom:-10px;">
      <router-link to="/" class="brand-link">
        <img
          src="./../assets/img/popcorn2.png"
          class="img-circle elevation-2"
          style="opacity: .8;width:150px"
        />

        <h1><b>PopCorn</b></h1>
      </router-link>
    </div>
    <div class="card">
      <div class="card-body login-card-body">
        <form>
          <div class="input-group mb-3">
            <input
              type="email"
              ref="email"
              class="form-control"
              placeholder="Email"
              v-model="data.email"
            />
            <div class="input-group-append">
              <span class="fas fa-envelope input-group-text"></span>
            </div>
          </div>
          <div class="input-group mb-3">
            <input
              type="password"
              ref="password"
              class="form-control"
              placeholder="password"
              v-model="data.password"
            />
            <div class="input-group-append">
              <span class="fas fa-lock input-group-text"></span>
            </div>
          </div>
          <div class="row">
            <div class="col-8">
              <div class="checkbox icheck">
                <router-link to="/register">
                  <b>Registre-se</b></router-link
                >
              </div>
            </div>
            <!-- /.col -->
            <div class="col-4">
              <button
                type="button"
                class="btn btn-primary btn-block btn-flat"
                @click.prevent="getFormValues()"
              >
                Logar
              </button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>
</template>

<script>
import { showWarning, showError, showSuccess } from '../utils/alerts';
import axios from '../utils/axios';
export default {
  name: 'Login',
  data: function() {
    return {
      data: {
        email: '',
        password: '',
      },
    };
  },
  mounted() {
    if (window.User.loggedIn()) {
      this.$router.push('/');
    } else {
      if (this.$develop) {
        this.data.email = this.$userDevelop;
        this.data.password = this.$passwordDevelop;
      }
    }
  },
  methods: {
    getFormValues() {
      if (this.checkValues()) {
        this.login();
      }
    },
    checkValues() {
      if (!this.data.email) {
        showWarning('Email Obrigatório!!');
        return false;
      }

      if (!this.data.password) {
        showWarning('Password Obrigatório!!');
        return false;
      }
      return true;
    },
    login() {
      axios
        .post('/user/auth', this.data)
        .then(response => {
          window.User.login(response.data);
          showSuccess('Login realizado com sucesso!');
        })
        .catch(err => {
          showError('Falha ao logar!!!');
          console.log(err);
        });
    },
  },
  created: function() {
    document.body.classList.add('login-page');
  },
  destroyed: function() {
    document.body.classList.remove('login-page');
  },
};
</script>

<style scoped></style>
