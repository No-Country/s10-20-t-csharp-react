import { Link } from "react-router-dom";

import notFoundImg from "../assets/not_found.jpg";
import { MainLayout } from "../layout/MainLayout";

export function NotFound() {
  return (
    <MainLayout>
      <div className="flex flex-col h-screen justify-center items-center gap-4">
        <img
          width="300"
          height="300"
          src={notFoundImg}
          alt="TV showing an 404 error message"
        />
        <h3 className="text-lg">La página que busca no existe</h3>
        <Link className="text-primary-100" to="/">
          Volver
        </Link>
      </div>
    </MainLayout>
  );
}
