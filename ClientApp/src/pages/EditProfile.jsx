import { Link } from "react-router-dom";

import { MainLayout } from "../layout/MainLayout";
import { EditProfileCard } from "../components/EditProfileCard";

import { LeftArrow } from "../components/atoms/leftArrow";

export function EditProfile() {
  return (
    <MainLayout>
      <section className="py-8 flex flex-col  items-center gap-8">
        <Link
          to="/profile"
          className="flex gap-4 items-center text-terciary-100"
        >
          Ir atrás <LeftArrow />
        </Link>
        <EditProfileCard />
      </section>
    </MainLayout>
  );
}
