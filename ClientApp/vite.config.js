import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';
import mkcert from 'vite-plugin-mkcert'

// https://vitejs.dev/config/
export default defineConfig({
    server: {
        proxy: {
            "/swagger": {
                target: "https://localhost:7125",
                changeOrigin: true,
                secure: false,
            },
             "/api": {
                target: "https://localhost:7125",
                changeOrigin: true,
                secure: false,
            }
        },
    },
    plugins: [react(), mkcert()],
});
