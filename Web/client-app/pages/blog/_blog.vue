<template>
  <div>
    <h1 v-text="blog == null ? 'No blog found' : blog.url"></h1>
  </div>
</template>

<script>
export default {
  async asyncData({ $axios, params }) {
    try {
      const data = await $axios.$get(`/api/blog/${params.blog}`)
      return {
        blog: data,
      }
    } catch {
      return { blog: null }
    }
  },
}
</script>

<style scoped></style>
