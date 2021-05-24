<template>
  <div class="container px-10">
    <div class="pb-5 border-b border-gray-200">
      <h3
        class="text-lg leading-6 font-medium text-gray-900"
        v-text="heading"
      ></h3>
    </div>

    <ul class="divide-y divide-gray-200">
      <li
        v-for="post in posts"
        :key="post.id"
        class="
          relative
          bg-white
          py-5
          px-4
          hover:bg-gray-50
          focus-within:ring-2
          focus-within:ring-inset
          focus-within:ring-indigo-600
        "
      >
        <div class="flex justify-between space-x-3">
          <div class="min-w-0 flex-1">
            <a href="#" class="block focus:outline-none">
              <span class="absolute inset-0" aria-hidden="true" />
              <p class="text-sm font-medium text-gray-900 truncate">
                {{ post.title }}
              </p>
            </a>
          </div>
        </div>
        <div class="mt-1">
          <p class="line-clamp-2 text-sm text-gray-600">
            {{ post.content }}
          </p>
        </div>
      </li>
    </ul>

    <div class="bg-white shadow px-4 py-5 sm:rounded-lg sm:p-6">
      <div class="md:grid md:grid-cols-3 md:gap-6">
        <div class="md:col-span-1">
          <h3 class="text-lg font-medium leading-6 text-gray-900">Post</h3>
          <!--          <p class="mt-1 text-sm text-gray-500">Post</p>-->
        </div>
        <div class="mt-5 md:mt-0 md:col-span-2">
          <form class="space-y-6" @submit.prevent="submit">
            <div class="grid grid-cols-3 gap-6">
              <div class="col-span-3 sm:col-span-2">
                <label
                  for="title"
                  class="block text-sm font-medium text-gray-700"
                >
                  Title
                </label>
                <div class="mt-1 flex rounded-md shadow-sm">
                  <input
                    id="title"
                    v-model="form.title"
                    type="text"
                    name="title"
                    class="
                      focus:ring-indigo-500
                      focus:border-indigo-500
                      flex-1
                      block
                      w-full
                      rounded-none rounded-r-md
                      sm:text-sm
                      border-gray-300
                    "
                  />
                </div>
              </div>
            </div>

            <div>
              <label
                for="content"
                class="block text-sm font-medium text-gray-700"
              >
                Content
              </label>
              <div class="mt-1">
                <textarea
                  id="content"
                  v-model="form.content"
                  name="content"
                  rows="3"
                  class="
                    shadow-sm
                    focus:ring-indigo-500
                    focus:border-indigo-500
                    block
                    w-full
                    sm:text-sm
                    border-gray-300
                    rounded-md
                  "
                />
              </div>
              <p class="mt-2 text-sm text-gray-500">Post content</p>
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
  async asyncData({ $axios, params }) {
    try {
      const blog = await $axios.$get(`/api/blog/${params.blog}`)
      return {
        blog,
      }
    } catch {
      return { blog: null }
    }
  },

  data() {
    return {
      form: {
        title: '',
        content: '',
      },
      posts: [],
    }
  },
  computed: {
    heading() {
      return this.blog == null ? 'No blog found' : this.blog.url
    },
  },

  mounted() {
    this.posts.push(...this.blog.posts.$values)
  },

  methods: {
    async submit() {
      const data = {
        ...this.form,
        blogId: this.blog.id,
      }

      const response = await this.$axios.$post('/api/post', data)

      if (!this.blog.posts) {
        this.blog.posts = []
      }

      this.form.content = null
      this.form.title = null
      this.posts.push(response)
    },
  },
}
</script>
