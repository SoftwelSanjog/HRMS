CREATE SEQUENCE public.Banks_id_seq;
ALTER TABLE public."Banks"
ALTER COLUMN "Id" SET DEFAULT nextval('public.Banks_id_seq'::regclass);


CREATE SEQUENCE public.Cities_id_seq;
ALTER TABLE public."Cities"
ALTER COLUMN "Id" SET DEFAULT nextval('public.Cities_id_seq'::regclass);

CREATE SEQUENCE public.Clusters_id_seq;
ALTER TABLE public."Clusters"
ALTER COLUMN "Id" SET DEFAULT nextval('public.Clusters_id_seq'::regclass);


CREATE SEQUENCE public.Countries_id_seq;
ALTER TABLE public."Countries"
ALTER COLUMN "Id" SET DEFAULT nextval('public.Countries_id_seq'::regclass);

CREATE SEQUENCE public.Designations_id_seq;
ALTER TABLE public."Designations"
ALTER COLUMN "Id" SET DEFAULT nextval('public.Designations_id_seq'::regclass);


CREATE SEQUENCE public.Employees_id_seq;
ALTER TABLE public."Employees"
ALTER COLUMN "Id" SET DEFAULT nextval('public.Employees_id_seq'::regclass);


CREATE SEQUENCE public.LeaveApplications_id_seq;
ALTER TABLE public."LeaveApplications"
ALTER COLUMN "Id" SET DEFAULT nextval('public.LeaveApplications_id_seq'::regclass);

CREATE SEQUENCE public.LeaveTypes_id_seq;
ALTER TABLE public."LeaveTypes"
ALTER COLUMN "Id" SET DEFAULT nextval('public.LeaveTypes_id_seq'::regclass);

CREATE SEQUENCE public.RoleProfiles_id_seq;
ALTER TABLE public."RoleProfiles"
ALTER COLUMN "Id" SET DEFAULT nextval('public.RoleProfiles_id_seq'::regclass);

CREATE SEQUENCE public.SystemCodeDetails_id_seq;
ALTER TABLE public."SystemCodeDetails"
ALTER COLUMN "Id" SET DEFAULT nextval('public.SystemCodeDetails_id_seq'::regclass);

CREATE SEQUENCE public.SystemCodes_id_seq;
ALTER TABLE public."SystemCodes"
ALTER COLUMN "Id" SET DEFAULT nextval('public.SystemCodes_id_seq'::regclass);


CREATE SEQUENCE public.SystemProfiles_id_seq;
ALTER TABLE public."SystemProfiles"
ALTER COLUMN "Id" SET DEFAULT nextval('public.SystemProfiles_id_seq'::regclass);

CREATE SEQUENCE public.AuditLogs_id_seq;
ALTER TABLE public."AuditLogs"
ALTER COLUMN "Id" SET DEFAULT nextval('public.AuditLogs_id_seq'::regclass);

