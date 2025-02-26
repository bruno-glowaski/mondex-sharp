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
                title="Create new"
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
      {{
        null /* Yes, this isn't particularly efficient, but I'm time constrained */
      }}
      {{
        null /* The correct thing would be to use only one dialog and multiplex it between the items */
      }}
      <v-dialog>
        <template #activator="{ props }">
          <v-btn
            v-bind="props"
            icon="mdi-pencil"
            aria-label="Update"
            class="me-2"
            size="small"
            color="secondary"
            variant="text"
          />
        </template>
        <template #default="{ isActive }">
          <FormsUpsertPokemonType
            title="Edit existing"
            @close="isActive.value = false"
            @submit="(value) => updateTypeById(item.id, value)"
          />
        </template>
      </v-dialog>
      <v-dialog max-width="500px">
        <template #activator="{ props }">
          <v-btn
            v-bind="props"
            icon="mdi-delete"
            aria-label="Delete"
            class="me-2"
            size="small"
            color="error"
            variant="text"
          />
        </template>
        <template #default="{ isActive }">
          <v-card>
            <v-card-title class="text-h5"
              >Are you sure you want to delete this item?</v-card-title
            >
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn variant="text" @click="isActive.value = false">
                Cancel
              </v-btn>
              <v-btn
                color="error"
                variant="flat"
                @click="
                  async () => {
                    await deleteTypeById(item.id!);
                    isActive.value = false;
                  }
                "
              >
                OK
              </v-btn>
              <v-spacer></v-spacer>
            </v-card-actions>
          </v-card>
        </template>
      </v-dialog>
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
  { key: "actions", title: "Actions", fixed: true, width: "200px" },
];

const { $backend } = useNuxtApp();
const {
  data: types,
  status,
  refresh,
  error,
} = await useBackend("/api/types", {
  query: { q: debouncedSearch },
});

const submitNewType = async (value: UpsertPokemonType) => {
  await $backend("/api/types", { method: "POST", body: value });
  refresh();
};

const updateTypeById = async (id: number, value: UpsertPokemonType) => {
  await $backend("/api/types/{id}", {
    method: "PUT",
    path: { id },
    body: value,
  });
  refresh();
};

const deleteTypeById = async (id: number) => {
  await $backend("/api/types/{id}", {
    method: "DELETE",
    path: { id },
  });
  refresh();
};
</script>
