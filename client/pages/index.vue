<template>
  <v-container>
    <v-card>
      <v-card-title>Categorias</v-card-title>
      <v-card-text>
        <v-tabs
          v-model="categoriaSelecionada"
          class="rounded"
          bg-color="grey-lighten-4"
          center-active
          grow
        >
          <v-tab
            v-for="categoria in categorias"
            :key="categoria.id"
            :value="categoria.id"
            @click="fetchProdutos(categoria.id)"
          >
            {{ categoria.nome }}
          </v-tab>
        </v-tabs>
        <v-btn
          to="/criar"
          class="my-3"
          rounded="xs"
          size="large"
          color="green"
          variant="flat"
          @click="() => (creationForm = true)"
          block
          >Adicionar produto</v-btn
        >
        <v-table>
          <thead>
            <tr class="font-weight-bold">
              <th class="text-left font-weight-bold">Nome</th>
              <th class="text-left font-weight-bold">Descrição</th>
              <th class="text-left font-weight-bold">Estoque</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="produto in produtos" :key="produto.id">
              <td>{{ produto.nome }}</td>
              <td>{{ produto.descricao }}</td>
              <td>{{ produto.estoque }}</td>
              <td class="text-center">
                <v-btn
                  class="mr-3"
                  density="comfortable"
                  icon="mdi-pencil"
                  variant="elevated"
                ></v-btn>
                <v-btn
                  density="comfortable"
                  icon="mdi-delete"
                  color="red"
                  @click="openDeleteDialog(produto.id)"
                ></v-btn>
              </td>
            </tr>
          </tbody>
        </v-table>
      </v-card-text>
    </v-card>

    <!-- Diálogo para confirmação do botão de excluir -->
    <v-dialog
      v-if="deleteDialog == true"
      v-model="deleteDialog"
      max-width="500px"
    >
      <v-card>
        <v-card-title class="headline">Confirme se quer excluir</v-card-title>
        <v-card-text
          >Você tem certeza de que deseja excluir esse produto?</v-card-text
        >
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="blue" variant="flat" text @click="deleteDialog = false"
            >Cancelar</v-btn
          >
          <v-btn
            color="red darken-1"
            variant="outlined"
            text
            @click="deleteProduto"
            >Excluir</v-btn
          >
        </v-card-actions>
      </v-card>
    </v-dialog>

    <product-form
      :categoria="categoriaSelecionada"
      v-if="creationForm"
    ></product-form>
  </v-container>
</template>

<script setup>
import { ref, onMounted } from "vue";
import axios from "axios";

const categorias = ref([]);
const produtos = ref([]);
const categoriaSelecionada = ref(null);
const deleteDialog = ref(false);
const creationForm = ref(false);
const produtoIdToDelete = ref(null);

const fetchCategorias = async () => {
  try {
    const response = await axios.get("https://localhost:7118/api/Categorias");
    categorias.value = response.data;
    if (categorias.value.length > 0) {
      categoriaSelecionada.value = categorias.value[0].id;
      fetchProdutos(categorias.value[0].id);
    }
  } catch (error) {
    console.error("Error fetching categories:", error);
  }
};

const fetchProdutos = async (categoriaId) => {
  try {
    const response = await axios.get(
      `https://localhost:7118/api/Categorias/${categoriaId}/produtos`
    );
    produtos.value = response.data;
  } catch (error) {
    console.error("Error fetching products:", error);
  }
};

const openDeleteDialog = (produtoId) => {
  produtoIdToDelete.value = produtoId;
  deleteDialog.value = true;
};

const deleteProduto = async () => {
  try {
    await axios.delete(
      `https://localhost:7118/api/produtos/${produtoIdToDelete.value}`
    );
    deleteDialog.value = false;
    fetchCategorias();
    produtos.value = products.value.filter(
      (product) => product.id !== produtoIdToDelete.value
    );
  } catch (error) {
    console.error("Error deleting product:", error);
  }
};

onMounted(fetchCategorias);
</script>

<style scoped></style>
