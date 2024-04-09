import { defineStore } from 'pinia';

export const useSessionStore = defineStore({
  id: 'session',
  state: () => ({
    isLoggedIn: false,
    username: '', // Nuevo campo para almacenar el nombre de usuario
  }),
  actions: {
    login(username: string) {
      // Lógica para iniciar sesión
      this.isLoggedIn = true;
      this.username = username; // Almacena el nombre de usuario
    },
    logout() {
      // Lógica para cerrar sesión
      this.isLoggedIn = false;
      this.username = ''; // Resetea el nombre de usuario al cerrar sesión
    },
  },
});
