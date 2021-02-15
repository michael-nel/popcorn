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
      <div class="card-body register-card-body">
        <form>
          <div class="input-group mb-3">
            <input
              type="text"
              class="form-control"
              ref="first"
              placeholder="Primeiro Nome"
            />
            <div class="input-group-append">
              <span class="fas fa-user input-group-text"></span>
            </div>
          </div>
          <div class="input-group mb-3">
            <input
              type="text"
              class="form-control"
              ref="last"
              placeholder="Último Nome"
            />
            <div class="input-group-append">
              <span class="fas fa-user input-group-text"></span>
            </div>
          </div>

          <div class="input-group mb-3">
            <input
              type="email"
              ref="email"
              class="form-control"
              placeholder="Email"
            />
            <div class="input-group-append">
              <span class="fas fa-envelope input-group-text"></span>
            </div>
          </div>
          <div class="input-group mb-3">
            <input
              type="password"
              class="form-control"
              ref="password"
              placeholder="Password"
            />
            <div class="input-group-append">
              <span class="fas fa-lock input-group-text"></span>
            </div>
          </div>
          <div class="input-group mb-3">
            <input
              type="password"
              class="form-control"
              ref="confirmation"
              placeholder="Confirme Password"
            />
            <div class="input-group-append">
              <span class="fas fa-lock input-group-text"></span>
            </div>
          </div>
          <div class="row">
            <div class="col-12">
              <button
                class="btn btn-primary btn-block btn-flat"
                @click.prevent="getFormValues()"
              >
                Registrar
              </button>
            </div>
          </div>
        </form>
        <br />
        <router-link to="login" class="text-center"
          >Volar Login</router-link
        >
      </div>
    </div>
  </div>
</template>

<script>
import { showWarning, showError, showSuccess } from '../utils/alerts';
import axios from '../utils/axios';
export default {
  name: 'Register',
  data: function() {
    return {
      data: {
        firstName: '',
        lastName: '',
        email: '',
        password: '',
      },
    };
  },
  methods: {
    getFormValues() {
      this.data.firstname = this.$refs.first.value;
      this.data.lastName = this.$refs.last.value;
      this.data.email = this.$refs.email.value;
      this.data.password = this.$refs.password.value;
      let retCheck = this.checkValues(this.$refs.confirmation.value);

      if (retCheck) {
        this.register();
      }
    },
    checkValues(confirmation) {
      if (!this.data.firstname) {
        showWarning('Primeiro Nome Obrigatório!!');
        return false;
      }

      if (!this.data.lastName) {
        showWarning('Último Nome Obrigatório!!');
        return false;
      }

      if (!this.data.email) {
        showWarning('Email Obrigatório!!');
        return false;
      }

      if (!this.data.password) {
        showWarning('Password Obrigatório!!');
        return false;
      }

      if (!confirmation) {
        showWarning('Confirmação Obrigatório!!');
        return false;
      }

      if (this.data.password != confirmation) {
        showError('Password diferente de Confirmação');
        return false;
      }

      return true;
    },
    register: function() {
      axios
        .post('/user', this.data)
        .then(response => {
          if (response.data.success) {
            showSuccess('Registro realizado com sucesso');
            this.$router.push({ path: '/login' });
          } else {
            const errors = response.data.notifications;
            showError(errors[0].message);
          }
        })
        .catch(err => showError(err));

      //
    },
  },
  created: function() {
    document.body.classList.add('register-page');
  },
  destroyed: function() {
    document.body.classList.remove('register-page');
  },
};
</script>

<style></style>
