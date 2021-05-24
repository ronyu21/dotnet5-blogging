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

              <!--              <div class="col-span-6 sm:col-span-3">-->
              <!--                <label-->
              <!--                  for="last_name"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >Last name</label-->
              <!--                >-->
              <!--                <input-->
              <!--                  id="last_name"-->
              <!--                  type="text"-->
              <!--                  name="last_name"-->
              <!--                  autocomplete="family-name"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    shadow-sm-->
              <!--                    sm:text-sm-->
              <!--                    border-gray-300-->
              <!--                    rounded-md-->
              <!--                  "-->
              <!--                />-->
              <!--              </div>-->

              <!--              <div class="col-span-6 sm:col-span-4">-->
              <!--                <label-->
              <!--                  for="email_address"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >Email address</label-->
              <!--                >-->
              <!--                <input-->
              <!--                  id="email_address"-->
              <!--                  type="text"-->
              <!--                  name="email_address"-->
              <!--                  autocomplete="email"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    shadow-sm-->
              <!--                    sm:text-sm-->
              <!--                    border-gray-300-->
              <!--                    rounded-md-->
              <!--                  "-->
              <!--                />-->
              <!--              </div>-->

              <!--              <div class="col-span-6 sm:col-span-3">-->
              <!--                <label-->
              <!--                  for="country"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >Country / Region</label-->
              <!--                >-->
              <!--                <select-->
              <!--                  id="country"-->
              <!--                  name="country"-->
              <!--                  autocomplete="country"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    py-2-->
              <!--                    px-3-->
              <!--                    border border-gray-300-->
              <!--                    bg-white-->
              <!--                    rounded-md-->
              <!--                    shadow-sm-->
              <!--                    focus:outline-none-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    sm:text-sm-->
              <!--                  "-->
              <!--                >-->
              <!--                  <option>United States</option>-->
              <!--                  <option>Canada</option>-->
              <!--                  <option>Mexico</option>-->
              <!--                </select>-->
              <!--              </div>-->

              <!--              <div class="col-span-6">-->
              <!--                <label-->
              <!--                  for="street_address"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >Street address</label-->
              <!--                >-->
              <!--                <input-->
              <!--                  id="street_address"-->
              <!--                  type="text"-->
              <!--                  name="street_address"-->
              <!--                  autocomplete="street-address"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    shadow-sm-->
              <!--                    sm:text-sm-->
              <!--                    border-gray-300-->
              <!--                    rounded-md-->
              <!--                  "-->
              <!--                />-->
              <!--              </div>-->

              <!--              <div class="col-span-6 sm:col-span-6 lg:col-span-2">-->
              <!--                <label-->
              <!--                  for="city"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >City</label-->
              <!--                >-->
              <!--                <input-->
              <!--                  id="city"-->
              <!--                  type="text"-->
              <!--                  name="city"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    shadow-sm-->
              <!--                    sm:text-sm-->
              <!--                    border-gray-300-->
              <!--                    rounded-md-->
              <!--                  "-->
              <!--                />-->
              <!--              </div>-->

              <!--              <div class="col-span-6 sm:col-span-3 lg:col-span-2">-->
              <!--                <label-->
              <!--                  for="state"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >State / Province</label-->
              <!--                >-->
              <!--                <input-->
              <!--                  id="state"-->
              <!--                  type="text"-->
              <!--                  name="state"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    shadow-sm-->
              <!--                    sm:text-sm-->
              <!--                    border-gray-300-->
              <!--                    rounded-md-->
              <!--                  "-->
              <!--                />-->
              <!--              </div>-->

              <!--              <div class="col-span-6 sm:col-span-3 lg:col-span-2">-->
              <!--                <label-->
              <!--                  for="postal_code"-->
              <!--                  class="block text-sm font-medium text-gray-700"-->
              <!--                  >ZIP / Postal</label-->
              <!--                >-->
              <!--                <input-->
              <!--                  id="postal_code"-->
              <!--                  type="text"-->
              <!--                  name="postal_code"-->
              <!--                  autocomplete="postal-code"-->
              <!--                  class="-->
              <!--                    mt-1-->
              <!--                    focus:ring-indigo-500-->
              <!--                    focus:border-indigo-500-->
              <!--                    block-->
              <!--                    w-full-->
              <!--                    shadow-sm-->
              <!--                    sm:text-sm-->
              <!--                    border-gray-300-->
              <!--                    rounded-md-->
              <!--                  "-->
              <!--                />-->
              <!--              </div>-->
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
