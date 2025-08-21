import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Typography,
} from "@mui/material";

type ConfirmDialogProps = {
  open: boolean;
  title?: string;
  description?: string;
  onConfirm: () => void;
  onCancel: () => void;
};

const ConfirmDialog = ({
  open,
  title = "Confirmar acción",
  description = "¿Estás seguro que deseas continuar?",
  onConfirm,
  onCancel,
}: ConfirmDialogProps) => {
  return (
    <Dialog open={open} onClose={onCancel}>
      <DialogTitle>{title}</DialogTitle>
      <DialogContent>
        <Typography variant="body2">{description}</Typography>
      </DialogContent>
      <DialogActions>
        <Button onClick={onCancel} variant="outlined">
          Cancelar
        </Button>
        <Button onClick={onConfirm} variant="contained" color="error" autoFocus>
          Confirmar
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default ConfirmDialog;
