module.exports = {
  root: true,
  env: {
    node: true
  },
  extends: ["plugin:vue/recommended", "@vue/prettier", "@vue/typescript"],
  rules: {
    "no-console": process.env.NODE_ENV === "production" ? "error" : "off",
    "no-debugger": process.env.NODE_ENV === "production" ? "error" : "off",
    "vue/max-attributes-per-line": [{ multiline: { allowFirstLine: true } }],
    "vue/mustache-interpolation-spacing": ["error", "never"]
  },
  parserOptions: {
    parser: "typescript-eslint-parser"
  }
};
