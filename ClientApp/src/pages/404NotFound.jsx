import { Link } from "react-router-dom";

export function NotFound() {
  return (
    <div className="flex flex-col h-screen justify-center items-center gap-4">
      <h1 className="text-error text-2xl font-bold">404</h1>
      <h3 className="text-lg">La página que busca no existe</h3>
      <Link className="text-primary-100" to="/">
        Volver
      </Link>
    </div>
  );
}
