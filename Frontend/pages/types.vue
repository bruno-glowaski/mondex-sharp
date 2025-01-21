<template>
  <v-data-table-server
    :loading="status === 'pending'"
    :headers="headers"
    :items="types ?? []"
    :items-length="types?.length ?? 0"
  >
    <template #top>
      <v-row class="pa-2">
        <v-col>
          <v-text-field
            v-model="search"
            density="compact"
            label="Search"
            prepend-inner-icon="mdi-magnify"
            variant="solo-filled"
            flat
            hide-details
          />
        </v-col>
        <v-col cols="2">
          <v-dialog>
            <template #activator="{ props }">
              <v-btn
                v-bind="props"
                block
                color="success"
                prepend-icon="mdi-plus"
              >
                Create new
              </v-btn>
            </template>
            <template #default="{ isActive }">
              <FormsUpsertPokemonType
                @close="isActive.value = false"
                @submit="submitNewType"
              />
            </template>
          </v-dialog>
        </v-col>
      </v-row>
    </template>
    <template #item.name="{ value }">
      <v-chip color="primary">{{ value }}</v-chip>
    </template>
    <template #item.actions="{ item }">
      <v-btn
        icon="mdi-delete"
        aria-label="Delete"
        class="me-2"
        size="small"
        color="error"
        variant="text"
        @click="console.log(item)"
      />
    </template>
  </v-data-table-server>
</template>

<script setup lang="ts">
import { FormsUpsertPokemonType } from "#components";
import { refDebounced } from "@vueuse/core";
import type { UpsertPokemonType } from "~/components/forms/upsert-pokemon-type.vue";

const search = ref("");
const debouncedSearch = refDebounced(search, 1000);
const headers = [
  { key: "id", title: "Id", fixed: true, width: 0 },
  { key: "name", title: "Name" },
  { key: "actions", title: "Actions", fixed: true },
];

const { $backend } = useNuxtApp();
const {
  data: types,
  status,
  refresh,
} = useBackend("/api/types", {
  query: { q: debouncedSearch },
});

const submitNewType = async (value: UpsertPokemonType) => {
  await $backend("/api/types", { method: "POST", body: value });
  refresh();
};
</script>
