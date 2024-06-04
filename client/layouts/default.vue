<template>
  <v-app>
    <v-app-bar app>
      <v-toolbar-title>ZDZCode</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-btn text to="/">Categorias</v-btn>
      <v-btn text to="/criar">Criar categoria</v-btn>
    </v-app-bar>
    <v-main>
      <NuxtPage />
    </v-main>
  </v-app>
</template>

<script setup></script>
