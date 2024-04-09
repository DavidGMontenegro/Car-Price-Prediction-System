import { useSessionStore } from "~/stores/session";

export default defineNuxtRouteMiddleware((to, from) => {
    const sessionStore = useSessionStore();
    
    if (!sessionStore.isLoggedIn) {
        return navigateTo('/account-managment');
    }
})