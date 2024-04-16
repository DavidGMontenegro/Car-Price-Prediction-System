<template>
  <div>
    <h1>ESTAMOS AQUÍ GENTE</h1>
    <div v-if="isAuthenticated">
      <p>Usuario autenticado: {{ user }}</p>
    </div>
    <div v-else>
      <p>No hay usuario autenticado.</p>
    </div>
  </div>
</template>

<script lang="ts">
import { useSessionStore } from "~/stores/session";

export default {
  setup() {
    const session = useSessionStore();

    definePageMeta({
      middleware: "auth",
    });

    return {
      isAuthenticated: session.isLoggedIn,
      user: session.username,
    };
  },
};
</script>
