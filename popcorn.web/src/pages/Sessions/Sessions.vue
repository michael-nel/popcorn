<template>
  <page title="Sessões">
    <div class="card">
      <div class="card-header">
        <router-link
          to="/sessions/ "
          class="btn btn-md btn-outline-primary"
          style="border-radius:50%"
        >
          <i class="nav-icon fas fa-plus"></i>
        </router-link>
        <div class="card-tools">
          <div class="float-right">
            <div class="input-group input-group-sm mt-1">
              <input
                class="form-control form-control-navbar"
                type="search"
                placeholder="Search"
                aria-label="Search"
                v-model="find"
                v-on:input="search"
              />
              <div class="input-group-append">
                <button class="btn btn-navbar" type="submit">
                  <i class="fas fa-search"></i>
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="card-body p-0">
        <table class="table table-hover">
          <thead>
            <tr>
              <th class="table-title">Filme</th>
              <th class="table-title">Sala</th>
              <th class="table-title">Data</th>
              <th class="table-title">Duração</th>
              <th style="width: 40px"></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in dataSearch" :key="item.id">
              <td>{{ item.movie.title }}</td>
              <td>{{ item.room.name }}</td>
              <td>{{ item.sessionStart }}</td>
              <td>{{ item.sessionEnd }}</td>
              <td>
                <div class="input-group input-group-sm mt-1">
                  <div class="input-group-append">
                    <button
                      class="btn btn-navbar"
                      @click="deleteMovie(item.id)"
                    >
                      <i class="fas fa-trash fa-danger"></i>
                    </button>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <div class="card-footer clearfix">
        <ul class="pagination pagination-sm m-0 float-right">
          <li
            class="page-item"
            v-for="itemPage in pages"
            :key="itemPage"
          >
            <a
              class="page-link"
              href="#"
              @click="getSessions(itemPage - 1)"
            >
              <u v-if="itemPage == selectPage">
                <span>{{ itemPage }}</span>
              </u>
              <span v-else>
                {{ itemPage }}
              </span>
            </a>
          </li>
        </ul>
      </div>
    </div>
  </page>
</template>

<script>
import Page from '../../components/Page';
import axios from '../../utils/axios';
import { showError, showSuccess } from '../../utils/alerts';

export default {
  name: 'Sessions',
  components: { Page },
  data: function() {
    return {
      find: '',
      data: [],
      dataSearch: [],
      pages: [],
      selectPage: 0,
    };
  },
  mounted() {
    this.getSessions(0);
  },
  methods: {
    getSessions(page) {
      axios
        .get('/session/' + page)
        .then(response => {
          this.data = this.dataSearch = this.convertList(
            response.data.sessions,
          );
          this.pages = [];
          for (let i = 1; i < response.data.totalPages; i++) {
            this.pages.push(i);
          }
          this.selectPage = page;
        })
        .catch(err => showError(err));
    },
    convertList(sessions) {
      sessions.forEach(item => {
        item.sessionStart = this.convertDateTime(item.sessionStart);
        item.sessionEnd =
          item.movie.duration.hours +
          'h ' +
          item.movie.duration.minutes +
          'min';
      });
      return sessions;
    },
    convertDateTime(data) {
      let dateConvert = new Date(data);
      return dateConvert.toLocaleString('pt-br');
    },
    deleteMovie(id) {
      if (confirm('Deseja realmente excluir o filme?')) {
        axios
          .delete('/session/' + id)
          .then(resp => {
            if (resp.data.success) {
              showSuccess('Sessão excluida com sucesso');
              this.getSessions(0);
            } else {
              showError(resp.data.notifications[0].message);
            }
          })
          .catch(err => showError(err));
      }
    },
    search() {
      if (this.find) {
        this.dataSearch = this.data.filter(res => {
          res = Object.values(res);
          for (let k = 0; k < res.length; k++) {
            if (
              (res[k] + '')
                .toLowerCase()
                .indexOf(this.find.toLowerCase()) >= 0
            ) {
              return true;
            }
          }
          return false;
        });
      } else {
        this.dataSearch = this.data;
      }
    },
  },
};
</script>
<style scoped>
.fa-primary {
  color: blue;
}
.fa-danger {
  color: red;
}
.table-title {
  font-size: 20px;
}
</style>
