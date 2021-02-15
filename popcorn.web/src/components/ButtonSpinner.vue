<template>
  <div
    v-show="visible"
    class="spinner-border spinner-border-sm"
    role="status"
  >
    <span class="sr-only">Loading...</span>
  </div>
</template>
<script>
export default {
  name: 'Spinner',
  props: ['visible'],
};
</script>
