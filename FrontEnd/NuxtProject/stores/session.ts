import { defineStore } from 'pinia';

export const useSessionStore = defineStore({
  id: 'session',
  state: () => ({
    isLoggedIn: false,
    username: '',
  }),
  actions: {
    login(username: string) {
      localStorage.username = username;
      this.isLoggedIn = true;
      this.username = username;
    },
    logout() {
      localStorage.removeItem('username');
      this.isLoggedIn = false;
      this.username = '';
    },
  },
});
