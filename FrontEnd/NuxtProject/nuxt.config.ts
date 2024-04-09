import type { NuxtConfig } from '@nuxt/types'
import { createPinia } from 'pinia'

const config: NuxtConfig = {
  buildModules: [
    '@nuxt/typescript-build',
    '@nuxtjs/style-resources', 
    '@pinia/nuxt',
  ],
  plugins: [
    { src:'~/plugins/pinia.js', ssr: false },
    { src:'~/plugins/element-plus.js', ssr: false },
  ],
  css: [
    'element-plus/dist/index.css',
  ],
  modules: [
    '@element-plus/nuxt',
  ],
  vite: {
    plugins: [createPinia()],
  },
  styleResources: {
    scss: [
      '~/assets/styles/variables.scss',
    ]
  }
}

export default config
