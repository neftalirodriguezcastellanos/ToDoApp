import { useEffect } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import Login from "./pages/login";
import Dashboard from "./pages/dashboard";
import { useAuth } from "./context/AuthContext";
import "./App.css";
import AppAlert from "./components/AppAlert";

function App() {
  const { isAuthenticated, alert, setAlert } = useAuth();

  useEffect(() => {
    // Si está autenticado y entra a /login, redirigir a /dashboard
    if (isAuthenticated && window.location.pathname === "/login") {
      window.location.href = "/dashboard";
    }
  }, [isAuthenticated]);

  return (
    <>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/login" element={<Login />} />
        <Route
          path="/dashboard"
          element={isAuthenticated ? <Dashboard /> : <Navigate to="/" />}
        />
      </Routes>
      {alert && <AppAlert alert={alert} onClose={() => setAlert(null)} />}
    </>
  );
}

export default App;
