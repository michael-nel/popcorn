<template>
  <form>
    <div class="card-body">
      <div class="form-group">
        <label for="dataId">Data e horário da Sessão</label>
        <input
          type="datetime-local"
          class="form-control"
          id="dataId"
          ref="dataStart"
        />
      </div>
      <div class="form-group">
        <label for="valueTicketId">Valor</label>
        <input
          type="number"
          class="form-control"
          id="valueTicketId"
          ref="ticketValue"
          placeholder="Valor do Ingresso"
        />
      </div>
      <div class="form-group">
        <label for="movieId"
          >Filme
          <span v-if="duration != ''">
            <code>({{ duration }})</code></span
          ></label
        >
        <select
          class="custom-select form-control-border"
          id="movieId"
          ref="movie"
          v-on:change="selectDuration()"
        >
          <option value="-1">Selecione</option>
          <option
            v-for="item in movies"
            :key="item.id"
            :value="item.id"
            >{{ item.title }}</option
          >
        </select>
      </div>
      <div class="form-group">
        <label for="animationId">Tipo Animação</label>
        <select
          class="custom-select form-control-border"
          id="animationId"
          ref="animation"
        >
          <option value="-1">Selecione</option>
          <option value="0">3d</option>
          <option value="1">2d</option>
        </select>
      </div>
      <div class="form-group">
        <label for="tipoAudio">Tipo Audio</label>
        <select
          class="custom-select form-control-border"
          id="tipoAudio"
          ref="audio"
        >
          <option value="-1">Selecione</option>
          <option value="0">Original</option>
          <option value="1">Dublado</option>
        </select>
      </div>
      <div class="form-group">
        <label for="roomId">Sala</label>
        <select
          class="custom-select form-control-border"
          id="roomId"
          ref="room"
        >
          <option value="-1">Selecione</option>
          <option
            v-for="item in rooms"
            :key="item.id"
            :value="item.id"
            >{{ item.name }}</option
          >
        </select>
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
// eslint-disable-next-line no-unused-vars
import ButtonSpinner from '../../components/ButtonSpinner';
import axios from '../../utils/axios';
import {
  showWarning,
  showError,
  showSuccess,
} from '../../utils/alerts';
export default {
  name: 'FormRoom',
  components: {
    'button-spinner': ButtonSpinner,
  },
  data: function() {
    return {
      loading: false,
      duration: '',
      rooms: [],
      movies: [],
      data: {
        SessionStart: '',
        SessionEnd: '',
        TicketValue: '',
        Animation: '',
        Audio: '',
        MovieId: '',
        RoomId: '',
      },
    };
  },
  mounted() {
    this.getMovies();
    this.getRooms();
  },
  methods: {
    getMovies() {
      axios
        .get('/movie/-1')
        .then(response => {
          this.movies = response.data.movies;
        })
        .catch(err => showError(err));
    },
    getRooms() {
      axios
        .get('/room')
        .then(response => {
          this.rooms = response.data.data;
        })
        .catch(err => showError(err));
    },
    leftPad(value, totalWidth, paddingChar) {
      var length = totalWidth - value.toString().length + 1;
      return Array(length).join(paddingChar || '0') + value;
    },
    selectDuration() {
      if (this.$refs.movie.value == '-1') {
        this.duration = '';
        this.SessionEnd = '';
        return;
      }

      let movie = this.movies.filter(
        x => x.id == this.$refs.movie.value,
      );

      this.data.SessionEnd =
        this.leftPad(movie[0].duration.hours, 2) +
        ':' +
        this.leftPad(movie[0].duration.minutes, 2);

      this.duration =
        movie[0].duration.hours +
        'h ' +
        movie[0].duration.minutes +
        'min';
    },
    getFormValues() {
      this.data.SessionStart = this.$refs.dataStart.value;
      this.data.TicketValue = Number(this.$refs.ticketValue.value);
      this.data.Animation = parseInt(this.$refs.animation.value);
      this.data.Audio = parseInt(this.$refs.audio.value);
      this.data.MovieId = this.$refs.movie.value;
      this.data.RoomId = this.$refs.room.value;
      if (this.checkValues()) {
        this.saveSession();
      }
    },
    checkValues() {
      if (!this.data.SessionStart) {
        showWarning('Data é Obrigatório!!');
        return false;
      }

      if (!this.data.TicketValue) {
        showWarning('Valor do ticket Obrigatório!!');
        return false;
      }

      if (this.data.MovieId == '-1') {
        showWarning('Filme é Obrigatório!!');
        return false;
      }

      if (this.data.Animation == '-1') {
        showWarning('Tipo de Animação Obrigatório!!');
        return false;
      }

      if (this.data.Audio == '-1') {
        showWarning('Tipo de Audio Obrigatório!!');
        return false;
      }

      if (this.data.RoomId == '-1') {
        showWarning('Sala é Obrigatório!!');
        return false;
      }

      return true;
    },
    saveSession() {
      this.loading = true;
      axios
        .post('/session', this.data)
        .then(response => {
          if (response.data.success) {
            showSuccess('Filme atualizado com sucesso');
            this.$router.push({ path: '/sessions' });
          } else {
            showError(response.data.notifications[0].message);
          }
        })
        .catch(err => {
          showError(err);
        })
        .finally((this.loading = false));
    },
  },
};
</script>
