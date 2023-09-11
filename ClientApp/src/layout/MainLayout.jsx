import { Footer } from "../components/footer";
import Navbar from "../components/navbar";

export function MainLayout({ children }) {
  return (
    <div>
      <Navbar />
      {children}
      <Footer />
    </div>
  );
}
