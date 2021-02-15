class Token {
  /*IsValid(token) {
    const payload = this.payload(token);
    if (payload) {
      let retorno = false;
      if (payload.iss === 'http://localhost:8000/api/auth/login') {
        retorno = true;
      }
      return retorno;
    }
    return false;
  }*/

  IsValid() {
    return true;
  }

  payload(token) {
    const payload = token.split('.')[1];
    return this.decode(payload);
  }

  decode(payload) {
    if (this.isBase64(payload)) {
      return JSON.parse(atob(payload));
    }
    return false;
  }

  isBase64(str) {
    try {
      return btoa(atob(str)).replace(/=/g, '') === str;
    } catch (err) {
      return false;
    }
  }
}
export default new Token();
