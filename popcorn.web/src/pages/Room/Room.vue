<template>
  <page title="Salas" column="col-md-6">
    <div class="card">
      <div class="card-body table-responsive p-0">
        <table class="table table-md">
          <thead>
            <tr>
              <th class="table-title">Salas</th>
              <th class="table-title text-right">
                Quantidade de Assentos
              </th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in data" :key="item.name">
              <td class="table-content">{{ item.name }}</td>
              <td
                class="text-right table-content"
                style="padding-right:120px"
              >
                <span
                  class="badge bg-success table-content badge-border"
                  >{{ item.chairs }}</span
                >
              </td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  </page>
</template>
<script>
import Page from '../../components/Page';
import axios from '../../utils/axios';
import { showError } from '../../utils/alerts';
export default {
  name: 'Room',
  data: function() {
    return {
      data: '',
    };
  },
  components: {
    Page,
  },
  mounted() {
    axios
      .get('/room')
      .then(response => {
        this.data = response.data.data;
      })
      .catch(err => showError(err));
  },
};
</script>

<style scoped>
.badge-border {
  border-radius: 50%;
}
.table-title {
  font-size: 23px;
}

.table-content {
  font-size: 18px;
}

.table-content-right {
  padding: 0 100 0 0;
}

.text-right {
  text-align: right;
}
</style>
