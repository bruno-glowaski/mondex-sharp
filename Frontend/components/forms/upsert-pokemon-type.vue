<template>
  <form @submit="onSubmit">
    <v-card :title="title">
      <v-card-text>
        <v-text-field
          label="Name"
          :error-messages="errors.name"
          v-bind="nameProps"
          v-model="name"
        />
      </v-card-text>

      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn type="submit" color="success" variant="flat"> Create </v-btn>
        <v-btn @click="emit('close')">Cancel</v-btn>
      </v-card-actions>
    </v-card>
  </form>
</template>

<script lang="ts">
export const UpsertPokemonTypeSchema = z.object({
  name: z.string().trim().nonempty(),
});
export type UpsertPokemonType = z.infer<typeof UpsertPokemonTypeSchema>;
</script>

<script setup lang="ts">
import { z } from "zod";

defineProps<{ title: string }>();
const emit = defineEmits<{
  (e: "submit", data: UpsertPokemonType): Promise<void>;
  (e: "close"): void;
}>();

const { handleSubmit, defineField, errors } = useForm({
  validationSchema: toTypedSchema(UpsertPokemonTypeSchema),
});

const [name, nameProps] = defineField("name");
const onSubmit = handleSubmit(async (values) => {
  await emit("submit", values);
  emit("close");
});
</script>
