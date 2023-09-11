import { MainLayout } from "../layout/MainLayout";
import { EditProfileCard } from "../components/EditProfileCard";

export function EditProfile() {
  return (
    <MainLayout>
      <section className="py-8 flex justify-center">
        <EditProfileCard />
      </section>
    </MainLayout>
  );
}
