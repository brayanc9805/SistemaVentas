import { DetalleVenta } from "./detalle-venta";

export interface Venta {
    idVenta?: number,
    numeroDocumento?: string,
    tipoPago: string,
    fechaRegistro?: string,
    total: string,
    detalleVenta: DetalleVenta[]
}
