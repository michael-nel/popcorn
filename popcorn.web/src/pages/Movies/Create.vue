<template>
  <page title="Filmes" column="col-md-6">
    <div class="card">
      <vue-form :data="data" />
    </div>
  </page>
</template>
<script>
import Page from '../../components/Page';
import Form from './Form';
import axios from '../../utils/axios';
import { showError } from '../../utils/alerts';
export default {
  name: 'CreateRoom',
  data: function() {
    return {
      data: {
        id: '',
        title: '',
        description: '',
        image: '',
        duration: '',
      },
    };
  },
  mounted() {
    var id = this.$route.params.id;
    if (id != ' ') {
      this.getMovie(id);
    }
  },
  components: {
    Page,
    'vue-form': Form,
  },
  methods: {
    getMovie(id) {
      axios
        .get('/movie/' + id)
        .then(resp => {
          this.data = resp.data.data;

          this.data.duration =
            this.leftPad(this.data.duration.hours, 2) +
            ':' +
            this.leftPad(this.data.duration.minutes, 2);
        })
        .catch(err => showError(err));
    },
    leftPad(value, totalWidth, paddingChar) {
      var length = totalWidth - value.toString().length + 1;
      return Array(length).join(paddingChar || '0') + value;
    },
  },
};
</script>
<style scoped></style>
