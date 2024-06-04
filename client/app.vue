<template>
  <NuxtLayout>
    <v-app>
      <v-navigation-drawer>
        <v-list-item
          title="ZDZCode"
          subtitle="Controle de estoque"
        ></v-list-item>
        <v-divider></v-divider>
        <v-list-item link to="/" title="Categorias"></v-list-item>
        <v-list-item link to="/criar" title="Criar Categoria"></v-list-item>
      </v-navigation-drawer>
    </v-app>
    <NuxtPage />
  </NuxtLayout>
</template>
