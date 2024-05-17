import { defineStore } from 'pinia';

export const useSessionStore = defineStore({
  id: 'session',
  state: () => ({
    isLoggedIn: false,
    username: '',
    token: "",
  }),
  actions: {
    login(username: string, token: string) {
      localStorage.username = username;
      localStorage.token = token;
      this.isLoggedIn = true;
      this.username = username;
      this.token = token;
    },
    logout() {
      localStorage.removeItem('username');
      this.isLoggedIn = false;
      this.username = '';
    },
  },
});
