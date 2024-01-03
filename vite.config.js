import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

export default defineConfig({
  plugins: [react()],
  root: "./src/FelizStopwatch.Client",
  build: {
    outDir: "./src/FelizStopwatch.Client/dist",
  }
})
