// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  devtools: { enabled: true },
  css: [
    "vuetify/lib/styles/main.sass", 
    'vuetify/styles',
    "@mdi/font/css/materialdesignicons.css"
 ],
 build: {
   transpile: [
     "vuetify"
   ],
  },
})
