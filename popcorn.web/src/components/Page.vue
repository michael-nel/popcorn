<template>
  <div>
    <section class="content-header">
      <div class="container-fluid">
        <div class="row mb-2 ml-4">
          <div class="col-sm-6">
            <h1>{{title}}</h1>
            <ol class="breadcrumb">
              <li class="breadcrumb-item"><a href="#">Home</a></li>
              <li class="breadcrumb-item active">{{title}}</li>
            </ol>
          </div>
        </div>
      </div><!-- /.container-fluid -->
    </section>
    <section class="content">
    <div class="container-fluid">
        <div class="row ml-4">
          <div :class="column">
            <slot></slot>
            </div>
        </div>
    </div>
    </section>
  </div>
</template>
<script>
export default {
  name: 'Page',
  props: ['title',"column"],
}
</script>

<style scoped>

</style>
