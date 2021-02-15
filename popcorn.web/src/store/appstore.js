class AppStore {
  storeToken(token) {
    sessionStorage.setItem('PopCornToken', token);
  }

  storeUser(user) {
    sessionStorage.setItem('PopCornUser', user);
  }

  store(user, token) {
    this.storeToken(token);
    this.storeUser(user);
  }

  clear() {
    sessionStorage.removeItem('PopCornToken');
    sessionStorage.removeItem('PopCornUser');
  }

  getToken() {
    return sessionStorage.getItem('PopCornToken');
  }

  getUser() {
    return sessionStorage.getItem('PopCornUser');
  }
}
export default new AppStore();
