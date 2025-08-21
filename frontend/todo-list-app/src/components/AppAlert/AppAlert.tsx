import { Alert, Collapse, IconButton } from "@mui/material";
import CloseIcon from "@mui/icons-material/Close";
import { useState, useEffect } from "react";
import type { AlertData } from "../../models/Interfaces";

interface AppAlertProps {
  alert?: AlertData;
  duration?: number;
  onClose?: () => void;
}

const AppAlert = ({ alert, duration = 5000, onClose }: AppAlertProps) => {
  const [open, setOpen] = useState(!!alert);

  useEffect(() => {
    if (alert && duration > 0) {
      const timer = setTimeout(() => setOpen(false), duration);
      return () => clearTimeout(timer);
    }
  }, [alert, duration]);

  const handleClose = () => {
    setOpen(false);
    onClose?.();
  };

  if (!alert) return null; // nada que mostrar

  return (
    <Collapse in={open}>
      <Alert
        severity={alert.severity || "error"}
        action={
          <IconButton
            aria-label="close"
            color="inherit"
            size="small"
            onClick={handleClose}
          >
            <CloseIcon fontSize="inherit" />
          </IconButton>
        }
        sx={{ mb: 2 }}
      >
        {alert.message}
      </Alert>
    </Collapse>
  );
};

export default AppAlert;
