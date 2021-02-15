import Token from './token';
import AppStore from './appstore';
import route from '../router';

class User {
  login(user) {
    console.log(user);
    if (Token.IsValid(user.accessToken)) {
      AppStore.store(user.firstName, user.accessToken);
      route.push('/');
    } else {
      route.push('/login');
    }
  }

  hasToken() {
    const storedToken = AppStore.getToken();
    if (storedToken) {
      return !!Token.IsValid(storedToken);
    }
    return false;
  }

  loggedIn() {
    return this.hasToken();
  }

  name() {
    if (this.loggedIn()) {
      return AppStore.getUser();
    }
  }

  token() {
    if (this.loggedIn()) {
      return AppStore.getToken();
    }
  }

  id() {
    if (this.loggedIn()) {
      const payload = Token.payload(AppStore.getToken());
      return payload.sub;
    }
  }

  logout() {
    AppStore.clear();
    route.push('/login');
  }
}
export default new User();
