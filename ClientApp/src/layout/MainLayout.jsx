import { Footer } from "../components/footer";
import Navbar from "../components/navbar";

export function MainLayout({ children }) {
  return (
    <div className="p2">
      <Navbar />
      {children}
      <Footer />
    </div>
  );
}
