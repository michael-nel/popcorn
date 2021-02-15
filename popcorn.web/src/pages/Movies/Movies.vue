<template>
  <page title="Filmes" column="col-md-10">
    <div class="card">
      <div class="card-header">
        <router-link
          to="/movies/ "
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
              <th class="table-title">Titulo</th>
              <th class="table-title">Descrição</th>
              <th class="table-title">Duração</th>
              <th style="width: 40px"></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in dataSearch" :key="item.id">
              <td>{{ item.title }}</td>
              <td>{{ item.description }}</td>
              <td>{{ item.duration }}</td>
              <td>
                <div class="input-group input-group-sm mt-1">
                  <div class="input-group-append">
                    <router-link
                      :to="'/movies/' + item.id"
                      class="btn btn-navbar mr-2"
                      style="border-radius:50%"
                    >
                      <i class="fas fa-edit fa-primary"></i>
                    </router-link>

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
              @click="getMovies(itemPage - 1)"
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
  name: 'Movies',
  data: function() {
    return {
      find: '',
      data: [],
      dataSearch: [],
      pages: [],
      selectPage: 0,
    };
  },
  components: {
    Page,
  },
  mounted() {
    this.getMovies(0);
  },
  methods: {
    getMovies(page) {
      axios
        .get('/movie/' + page)
        .then(response => {
          this.data = this.dataSearch = this.convertList(
            response.data.movies,
          );
          this.pages = [];
          for (let i = 1; i < response.data.totalPages; i++) {
            this.pages.push(i);
          }
          this.selectPage = page;
        })
        .catch(err => showError(err));
    },
    deleteMovie(id) {
      if (confirm('Deseja realmente excluir o filme?')) {
        axios
          .delete('/movie/' + id)
          .then(response => {
            if (response.data.success) {
              showSuccess('Filme excluido com sucesso');
              this.getMovies(0);
            } else {
              showError(response.data.notifications[0].message);
            }
          })
          .catch(err => showError(err));
      }
    },
    convertList(movies) {
      movies.forEach(item => {
        item.duration = this.convertToHourMinute(item.duration);
      });
      return movies;
    },
    convertToHourMinute(data) {
      return data.hours + 'h ' + data.minutes + 'min';
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
