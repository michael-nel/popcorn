<template>
  <aside class="main-sidebar sidebar-dark-primary elevation-4">
    <!-- Brand Logo -->
    <router-link to="/" class="brand-link">
      <img
        src="./../../assets/img/popcorn2.png"
        class="brand-image img-circle elevation-2"
        style="opacity: .8"
      />
      <span class="brand-text font-weight-light">PopCorn</span>
    </router-link>

    <!-- Sidebar -->
    <div class="sidebar">
      <!-- Sidebar user (optional) -->
      <div class="user-panel mt-3 pb-3 mb-3 d-flex">
        <div class="image">
          <img
            src="./../../assets/img/default-user.png"
            class="img-circle elevation-2"
            alt="User Image"
          />
        </div>
        <div class="info">
          <router-link to="/profile" class="d-block">{{
            user
          }}</router-link>
        </div>
      </div>

      <!-- Sidebar Menu -->
      <nav class="mt-2">
        <ul
          class="nav nav-pills nav-sidebar flex-column"
          data-widget="treeview"
          role="menu"
          data-accordion="false"
        >
          <!-- Add icons to the links using the .nav-icon class
          with font-awesome or any other icon font library-->

          <li class="nav-item has-treeview">
            <router-link to="/room" class="nav-link">
              <i class="nav-icon fas fa-share-alt"></i>
              <p>Salas</p>
            </router-link>
          </li>
          <li class="nav-item has-treeview">
            <router-link to="/movies" class="nav-link">
              <i class="nav-icon fas fa-film"></i>
              <p>Filmes</p>
            </router-link>
          </li>
          <li class="nav-item has-treeview">
            <router-link to="/sessions" class="nav-link">
              <i class="nav-icon fas fa-play-circle"></i>
              <p>Sessões</p>
            </router-link>
          </li>
        </ul>
      </nav>
      <!-- /.sidebar-menu -->
    </div>
    <!-- /.sidebar -->
  </aside>
</template>

<script>
export default {
  name: 'SidebarMenu',
  data: function() {
    return {
      isMenuOpen: false,
      user: '',
    };
  },
  beforeMount() {
    this.user = window.User.name();
  },
  computed: {
    authUser() {
      return this.$store.getters.getAuthUser;
    },
  },
  methods: {
    toogleMenu: function(event) {
      event.stopPropagation();
      event.preventDefault();
      this.isMenuOpen = !this.isMenuOpen;
    },
  },
};
</script>

<style></style>
