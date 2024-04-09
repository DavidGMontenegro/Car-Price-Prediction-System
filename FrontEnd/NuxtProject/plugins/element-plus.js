import ElementPlus from 'element-plus';
import * as ElementPlusIconsVue from '@element-plus/icons-vue';

export default defineNuxtPlugin((nuxtApp) => {
    if (process.client) {
        nuxtApp.vueApp.use(ElementPlus);
        
        for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
            nuxtApp.vueApp.component(key, component);
        }
    }
});
