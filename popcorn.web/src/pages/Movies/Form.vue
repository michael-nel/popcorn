<template>
  <form>
    <div class="card-body">
      <div class="form-group">
        <label for="tituloId">Titulo</label>
        <input
          type="text"
          class="form-control"
          id="tituloId"
          ref="title"
          v-model="data.title"
          placeholder="Titulo"
        />
      </div>
      <div class="form-group">
        <label for="descricaoId">Descrição</label>
        <input
          type="text"
          class="form-control"
          id="descricaoId"
          ref="description"
          v-model="data.description"
          placeholder="Descrição"
        />
      </div>
      <div class="form-group">
        <label for="duracaoId">Duração</label>
        <input
          type="time"
          class="form-control"
          id="duracaoId"
          ref="duration"
          v-model="data.duration"
        />
      </div>
      <div class="form-group">
        <label for="imagemId"
          >Imagem
          <small v-if="data.image"
            ><code>{{ data.image }}</code></small
          ></label
        >

        <div class="input-group">
          <div class="custom-file">
            <input
              type="file"
              class="custom-file-input"
              ref="image"
              accept="image/png, image/jpeg"
              id="imagemId"
              v-on:change="setFileName"
            />
            <label class="custom-file-label" for="imagemId"
              >Selecione a Image</label
            >
          </div>
          <div class="input-group-append">
            <span class="input-group-text">Upload</span>
          </div>
        </div>
      </div>
    </div>
    <div class="card-footer">
      <router-link to="/movies" class="link">
        Voltar
      </router-link>
      <button
        @click.prevent="getFormValues()"
        class="btn btn-primary float-right"
        :disabled="loading"
      >
        <button-spinner :visible="loading" />
        Salvar
      </button>
    </div>
  </form>
</template>
<script>
import ButtonSpinner from '../../components/ButtonSpinner';
import {
  showWarning,
  showError,
  showSuccess,
} from '../../utils/alerts';
import axios from '../../utils/axios';
export default {
  name: 'FormRoom',
  props: ['data'],
  components: {
    'button-spinner': ButtonSpinner,
  },
  data: function() {
    return {
      loading: false,
      upload: false,
    };
  },
  methods: {
    getFormValues() {
      this.data.title = this.$refs.title.value;
      this.data.description = this.$refs.description.value;
      this.data.duration = this.$refs.duration.value;

      if (this.upload) this.data.image = this.$refs.image.value;

      let retCheck = this.checkValues();

      if (retCheck) {
        this.uploadFile();
      }
    },
    checkValues() {
      if (!this.data.title) {
        showWarning('Titulo Obrigatório!!');
        return false;
      }

      if (!this.data.description) {
        showWarning('Descrição Obrigatório!!');
        return false;
      }

      if (!this.data.duration) {
        showWarning('Duração Obrigatório!!');
        return false;
      }

      if (!this.data.id && !this.data.image) {
        showWarning('Imagem Obrigatório!!');
        return false;
      }

      return true;
    },
    uploadFile() {
      if (!this.upload) {
        this.saveMovie();
        return;
      }

      var formData = new FormData();
      formData.append('file', this.$refs.image.files[0]);
      axios
        .post('/upload', formData, {
          headers: {
            'Content-Type': 'multipart/form-data',
          },
        })
        .then(res => {
          this.data.image = res.data;
          this.saveMovie();
        })
        .catch(err => showError(err));
    },
    saveMovie() {
      this.loading = true;
      if (!this.data.id) {
        axios
          .post('/movie', this.data)
          .then(response => {
            console.log(response);
            this.$router.push({ path: '/movies' });
          })
          .catch(err => showError(err))
          .finally((this.loading = false));
      } else {
        axios
          .put('/movie', this.data)
          .then(response => {
            showSuccess('Filme atualizado com sucesso');
            console.log(response);
            this.$router.push({ path: '/movies' });
          })
          .catch(err => showError(err))
          .finally((this.loading = false));
      }
    },
    setFileName() {
      this.upload = true;
    },
  },
};
</script>
