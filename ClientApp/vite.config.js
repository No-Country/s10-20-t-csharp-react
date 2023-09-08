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
            },
            "/auth2": {
                target: "https://localhost:7125",
                changeOrigin: true,
                secure: false,
            },
            "/Auth2": {
                target: "https://localhost:7125",
                changeOrigin: true,
                secure: false,
            }
        },
    },
    build: {
        rollupOptions: {
            output: {
                manualChunks(id) {
                    if (id.includes('node_modules')) {
                        return id.toString().split('node_modules/')[1].split('/')[0].toString();
                    }
                }
            }
        }
    },
    plugins: [react(), mkcert()],
});
