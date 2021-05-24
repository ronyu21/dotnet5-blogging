<template>
  <div class="container">
    <div class="bg-white shadow overflow-hidden sm:rounded-md">
      <ul class="divide-y divide-gray-200">
        <li v-for="blog in blogs" :key="blog.id" class="px-4 py-4 sm:px-6">
          <nuxt-link :to="'/blog/' + blog.id" v-text="blog.url"></nuxt-link>
        </li>
      </ul>
    </div>

    <hr class="h-1 bg-gray-200 my-4" />
    <div class="bg-white shadow px-4 py-5 sm:rounded-lg sm:p-6">
      <div class="md:grid md:grid-cols-3 md:gap-6">
        <div class="md:col-span-1">
          <h3 class="text-lg font-medium leading-6 text-gray-900">
            Blog Information
          </h3>
          <p class="mt-1 text-sm text-gray-500">Add blog information</p>
        </div>
        <div class="mt-5 md:mt-0 md:col-span-2">
          <form @submit.prevent="submit">
            <div class="grid grid-cols-6 gap-6">
              <div class="col-span-6 sm:col-span-3">
                <label for="url" class="block text-sm font-medium text-gray-700"
                  >Url</label
                >
                <input
                  id="url"
                  v-model="form.url"
                  type="text"
                  name="url"
                  autocomplete="given-name"
                  class="
                    mt-1
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    shadow-sm
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
            </div>
            <button
              type="submit"
              class="
                inline-flex
                items-center
                px-5
                py-2
                border border-transparent
                text-base
                font-medium
                rounded-full
                shadow-sm
                text-white
                bg-indigo-600
                hover:bg-indigo-700
                focus:outline-none
                focus:ring-2 focus:ring-offset-2 focus:ring-indigo-500
              "
            >
              Add
            </button>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  async asyncData({ $axios }) {
    try {
      const data = await $axios.$get('/api/blog')
      return {
        blogs: data,
      }
    } catch {
      return { blogs: [] }
    }
  },

  data() {
    return {
      form: {
        url: '',
      },
    }
  },

  methods: {
    async submit() {
      const response = await this.$axios.$post('/api/blog', { ...this.form })
      this.form.url = null
      this.blogs.push(response)
    },
  },
}
</script>
