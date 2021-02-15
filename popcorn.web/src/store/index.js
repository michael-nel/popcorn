export default {
  state: {
    item: {},
    acionamentos: false,
    filtros: {},
    result: {},
  },
  mutations: {
    setItem(state, obj) {
      state.item = obj || {};
    },
    setAcionamentos(state, obj) {
      state.acionamentos = obj || {};
    },
    setResult(state, obj) {
      state.result = obj || {};
    },
    setFiltros(state, obj) {
      state.filtros = obj || {};
    },
  },
};
