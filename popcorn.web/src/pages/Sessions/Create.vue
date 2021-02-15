<template>
  <page title="Sessoes" column="col-md-6">
    <div class="card">
      <vue-form />
    </div>
  </page>
</template>
<script>
import Page from '../../components/Page';
import Form from './Form';
export default {
  name: 'CreateSession',
  components: {
    Page,
    'vue-form': Form,
  },
};
</script>
<style scoped></style>
